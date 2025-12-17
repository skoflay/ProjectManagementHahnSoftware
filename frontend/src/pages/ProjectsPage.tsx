
import { useEffect, useState } from 'react';
import type { Project } from '../types/project.types';
import { ProjectService } from '../services/project.service';
import { useNavigate } from 'react-router-dom';

const ProjectsPage = () => {
  const [projects, setProjects] = useState<Project[]>([]);
  const [loading, setLoading] = useState(false);
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

  const handleDelete = async (projectId: string) => {
    if (!confirm('Are you sure you want to delete this project?')) return;
    try {
      await ProjectService.delete(projectId);
      setProjects(projects.filter(p => p.id !== projectId));
    } catch (err) {
      console.error(err);
      alert('Failed to delete project');
    }
  };

  const handleEdit = (project: Project) => {
    
    navigate(`/projects/edit/${project.id}`);
  };

  return (
    <div className="container mt-4">
      <h2 className="mb-4">My Projects</h2>

      {loading && <p>Loading...</p>}
      {error && <p className="text-danger">{error}</p>}

      <div className="row">
        {projects.map(project => (
          <div className="col-md-4 mb-3" key={project.id}>
            <div className="card shadow-sm">
              <div className="card-body">
                <h5 className="card-title">{project.title}</h5>
                <p className="card-text">{project.description || 'No description'}</p>
                <button
                  className="btn btn-primary me-2"
                  onClick={() => handleEdit(project)}
                >
                  Edit
                </button>
                <button
                  className="btn btn-danger"
                  onClick={() => handleDelete(project.id)}
                >
                  Delete
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
