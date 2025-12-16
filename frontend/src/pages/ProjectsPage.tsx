import { useEffect, useState } from 'react';
import type { Project } from '../types/project.types';
import { ProjectService } from '../services/project.service';
import { useNavigate } from 'react-router-dom';

const ProjectsPage = () => {
  const [projects, setProjects] = useState<Project[]>([]);
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  const navigate = useNavigate();

  useEffect(() => {
    loadProjects();
  }, []);

  const loadProjects = async () => {
    try {
      setLoading(true);
      const data = await ProjectService.getAll();
      setProjects(data);
    } catch (err) {
      setError('Failed to load projects');
      console.log(err);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="container mt-4">
      <h2 className="mb-4">My Projects</h2>

      {loading && <p>Loading...</p>}
      {error && <p className="text-danger">{error}</p>}

      {!loading && !error && projects.length === 0 && (
        <p>No projects found</p>
      )}

      <div className="row">
        {projects.map(project => (
          <div className="col-md-4 mb-3" key={project.id}>
            <div className="card shadow-sm h-100">
              <div className="card-body d-flex flex-column">
                <h5 className="card-title">{project.title}</h5>

                <p className="card-text flex-grow-1">
                  {project.description || 'No description'}
                </p>

                
                <button
                  className="btn btn-primary btn-sm mt-3"
                  onClick={() => navigate(`/projects/${project.id}/tasks`)}
                >
                  View Tasks
                </button>
              </div>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};

export default ProjectsPage;
