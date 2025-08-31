using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private static readonly ConcurrentDictionary<int, User> users = new();
    private static int userIdCounter = 0;
    private static readonly object idLock = new();

    [HttpGet]
    public ActionResult<IEnumerable<User>> GetAllUsers([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var pagedUsers = users.Values
                              .Skip((page - 1) * pageSize)
                              .Take(pageSize)
                              .ToList();
        return Ok(pagedUsers);
    }

    [HttpGet("{id}")]
    public ActionResult<User> GetUserById(int id)
    {
        if (users.TryGetValue(id, out var user))
            return Ok(user);
        return NotFound();
    }

    [HttpPost]
    public ActionResult<User> CreateUser([FromBody] User newUser)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Check for duplicate email
        if (users.Values.Any(u => u.Email.Equals(newUser.Email, StringComparison.OrdinalIgnoreCase)))
            return Conflict(new { error = "A user with this email already exists." });

        // Thread-safe ID generation
        lock (idLock)
        {
            newUser.Id = ++userIdCounter;
        }

        if (!users.TryAdd(newUser.Id, newUser))
            return StatusCode(500, new { error = "Failed to create user due to concurrency issue." });

        return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, newUser);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateUser(int id, [FromBody] User updatedUser)
    {
        if (!users.ContainsKey(id))
            return NotFound();

        // Check for email conflict with other users
        if (users.Values.Any(u => u.Id != id && u.Email.Equals(updatedUser.Email, StringComparison.OrdinalIgnoreCase)))
            return Conflict(new { error = "Another user with this email already exists." });

        users[id] = new User
        {
            Id = id,
            FullName = updatedUser.FullName,
            Email = updatedUser.Email,
            Department = updatedUser.Department
        };

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        if (!users.TryRemove(id, out _))
            return NotFound();

        return NoContent();
    }
}