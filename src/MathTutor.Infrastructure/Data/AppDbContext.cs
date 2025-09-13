using Microsoft.EntityFrameworkCore;
using MathTutor.Domain.Entities;

namespace MathTutor.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<MathProblem> MathProblems => Set<MathProblem>();
    public DbSet<TestSession> TestSessions => Set<TestSession>();
    public DbSet<TestSessionQuestion> TestSessionQuestions => Set<TestSessionQuestion>();

}
