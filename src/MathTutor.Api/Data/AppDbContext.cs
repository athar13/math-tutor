using Microsoft.EntityFrameworkCore;
using MathTutor.Api.Models;

namespace MathTutor.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<MathProblem> MathProblems => Set<MathProblem>();
    public DbSet<Progress> Progress => Set<Progress>();
    public DbSet<TestSession> TestSessions => Set<TestSession>();
    public DbSet<TestSessionQuestion> TestSessionQuestions => Set<TestSessionQuestion>();

}