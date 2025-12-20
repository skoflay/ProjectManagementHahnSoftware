import { useEffect, useState } from 'react';
import type { Project } from '../types/project.types';
import { ProjectService } from '../services/project.service';
import { useNavigate } from 'react-router-dom';
import { ProjectForm } from '../components/ProjectForm';
import { ProjectUpdateForm } from '../components/ProjectUpdateForm';
import { ProjectProgressBar } from '../components/ProjectProgressBar';
import { SearchInput } from '../components/SearchInput';
import '../styles/projects.css';
import { Edit2, Trash2, FileText, Plus } from 'lucide-react';

export const ProjectsPage = () => {
  const [projects, setProjects] = useState<Project[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const [showCreateForm, setShowCreateForm] = useState(false);
  const [editingProjectId, setEditingProjectId] = useState<string | null>(null);

  const [search, setSearch] = useState('');
  const [page, setPage] = useState(1);
  const pageSize = 6;
  const [totalItems, setTotalItems] = useState(0);

  const navigate = useNavigate();

  useEffect(() => { loadProjects(page); }, [page]);
  useEffect(() => { loadProjects(1); }, [search]);

  const loadProjects = async (pageNumber: number) => {
    try {
      setLoading(true);
      const data = await ProjectService.getPaged(pageNumber, pageSize, search || undefined);
      setProjects(data.items);
      setTotalItems(data.totalItems);
      setPage(data.page);
    } catch (err) {
      setError('Failed to load projects');
      console.error(err);
    } finally { setLoading(false); }
  };

  const handleDelete = async (projectId: string) => {
    if (!confirm('Are you sure?')) return;
    await ProjectService.delete(projectId);
    loadProjects(page);
  };

  const handleDetails = (projectId: string) => {
    navigate(`/projects/${projectId}/tasks`);
  };

  const handleCreated = () => { loadProjects(1); setShowCreateForm(false); };
  const handleUpdated = () => { loadProjects(page); setEditingProjectId(null); };

  const totalPages = Math.ceil(totalItems / pageSize);

  return (
    <div className="container mt-4">
      <h2 className="mb-3">My Projects</h2>

      <SearchInput value={search} onChange={setSearch} placeholder="Search project by title..." />

      <button
        className="btn btn-success mb-3 d-flex align-items-center"
        onClick={() => setShowCreateForm(!showCreateForm)}
      >
        <Plus size={16} className="me-2" /> {showCreateForm ? 'Cancel' : 'Create Project'}
      </button>

      {showCreateForm && <ProjectForm onCreated={handleCreated} onCancel={() => setShowCreateForm(false)} />}

      {loading && <p>Loading...</p>}
      {error && <p className="text-danger">{error}</p>}

      <div className="row">
        {projects.map(project => (
          <div className="col-md-4 mb-4" key={project.id}>
            <div className="card shadow-sm project-card">
              <div className="card-body d-flex flex-column">
                {editingProjectId === project.id ? (
                  <ProjectUpdateForm project={project} onUpdated={handleUpdated} onCancel={() => setEditingProjectId(null)} />
                ) : (
                  <>
                    <h5 className="card-title">{project.title}</h5>
                    <p className="card-text">{project.description || 'No description'}</p>

                    <ProjectProgressBar projectId={project.id} />

                    <div className="mt-auto d-flex justify-content-end gap-2">
  <button 
    className="btn btn-sm btn-primary" 
    onClick={() => setEditingProjectId(project.id)}
    aria-label={`Edit ${project.title}`}
  >
    <Edit2 size={16} />
  </button>
  <button 
    className="btn btn-sm btn-danger" 
    onClick={() => handleDelete(project.id)}
    aria-label={`Delete ${project.title}`}
  >
    <Trash2 size={16} />
  </button>
  <button 
    className="btn btn-sm btn-info" 
    onClick={() => handleDetails(project.id)}
    aria-label={`View details for ${project.title}`}
  >
    <FileText size={16} />
  </button>
</div>
                  </>
                )}
              </div>
            </div>
          </div>
        ))}
      </div>

      {totalPages > 1 && (
        <div className="d-flex justify-content-between mt-3">
          <button className="btn btn-secondary" disabled={page <= 1} onClick={() => loadProjects(page - 1)}>Previous</button>
          <span>Page {page} of {totalPages}</span>
          <button className="btn btn-secondary" disabled={page >= totalPages} onClick={() => loadProjects(page + 1)}>Next</button>
        </div>
      )}
    </div>
  );
};

export default ProjectsPage;
