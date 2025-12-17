import { useEffect, useState } from 'react';
import type { Project } from '../types/project.types';
import { ProjectService } from '../services/project.service';
import { useNavigate } from 'react-router-dom';
import { ProjectForm } from '../components/ProjectForm';
import { ProjectUpdateForm } from '../components/ProjectUpdateForm';
import { ProjectProgressBar } from '../components/ProjectProgressBar';

export const ProjectsPage = () => {
  const [projects, setProjects] = useState<Project[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [showCreateForm, setShowCreateForm] = useState(false);
  const [editingProjectId, setEditingProjectId] = useState<string | null>(null);

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
      console.error(err);
    } finally {
      setLoading(false);
    }
  };

  const handleDelete = async (projectId: string) => {
    if (!confirm('Are you sure?')) return;
    await ProjectService.delete(projectId);
    setProjects(projects.filter(p => p.id !== projectId));
  };

  const handleDetails = (projectId: string) => {
    navigate(`/projects/${projectId}/tasks`);
  };

  const handleCreated = (project: Project) => {
    setProjects([...projects, project]);
    setShowCreateForm(false);
  };

  const handleUpdated = (updated: Project) => {
    setProjects(projects.map(p => p.id === updated.id ? updated : p));
    setEditingProjectId(null);
  };

  return (
    <div className="container mt-4">
      <h2 className="mb-4">My Projects</h2>

      <button className="btn btn-success mb-3" onClick={() => setShowCreateForm(!showCreateForm)}>
        {showCreateForm ? 'Cancel' : '➕ Create Project'}
      </button>

      {showCreateForm && <ProjectForm onCreated={handleCreated} onCancel={() => setShowCreateForm(false)} />}

      {loading && <p>Loading...</p>}
      {error && <p className="text-danger">{error}</p>}

      <div className="row">
        {projects.map(project => (
          <div className="col-md-4 mb-3" key={project.id}>
            <div className="card shadow-sm">
              <div className="card-body">
                {editingProjectId === project.id ? (
                  <ProjectUpdateForm
                    project={project}
                    onUpdated={handleUpdated}
                    onCancel={() => setEditingProjectId(null)}
                  />
                ) : (
                  <>
                    <h5 className="card-title">{project.title}</h5>
                    <p className="card-text">{project.description || 'No description'}</p>
                     <ProjectProgressBar projectId={project.id} />
                    <button className="btn btn-sm btn-primary me-2" onClick={() => setEditingProjectId(project.id)}>✏️</button>
                    <button className="btn btn-sm btn-danger me-2" onClick={() => handleDelete(project.id)}>🗑️</button>
                    <button className="btn btn-sm btn-info" onClick={() => handleDetails(project.id)}>📄</button>
                  </>
                )}
              </div>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};
export default ProjectsPage