using System.Collections.Generic;
using System.Threading.Tasks;
using HdProduction.Dashboard.Application.Models;
using HdProduction.Dashboard.Application.NpgsqlOrm;
using HdProduction.Dashboard.Domain.Entities;
using HdProduction.Dashboard.Domain.Exceptions;

namespace HdProduction.Dashboard.Application.Queries.Projects
{
  public interface IProjectQuery
  {
    Task<ProjectReadModel> GetAsync(long id);
    Task<IReadOnlyCollection<ProjectGridReadModel>> GetAllAsync(long userId);
  }

  public class ProjectQuery : IProjectQuery
  {
    private static readonly string SelectColumns = string.Join(',', ProjectMetadata.All);
    private readonly IDatabaseConnector _database;

    public ProjectQuery(IDatabaseConnector database)
    {
      _database = database;
    }

    public async Task<ProjectReadModel> GetAsync(long id)
    {
      using (var context = _database.NewDataContext())
      {
        return await context.Sql($"SELECT {SelectColumns} FROM project WHERE {ProjectMetadata.Id} = @Id")
          .Set("Id", id)
          .ReadOrDefaultAsync(r => new ProjectReadModel
          {
            Id = r.Get<long>("Id"),
            Name = r.Get<string>("Name")
          })
          ?? throw  new EntityNotFoundException("Project not found");
      }
    }

    public async Task<IReadOnlyCollection<ProjectGridReadModel>> GetAllAsync(long userId)
    {
      using (var context = _database.NewDataContext())
      {
        return await context.Sql($@"SELECT {ProjectMetadata.Id}, {ProjectMetadata.Name} 
                                FROM {ProjectMetadata.Table} JOIN {UserProjectRightsMetadata.Table} 
                                ON {ProjectMetadata.Id} = {UserProjectRightsMetadata.ProjectId}
                                WHERE {UserProjectRightsMetadata.UserId} = @UserId")
          .Set("UserId", userId)
          .ReadAllAsync(r => new ProjectGridReadModel
          {
            Id = r.Get<long>("Id"),
            Name = r.Get<string>("Name")
          });
      }
    }
  }
}