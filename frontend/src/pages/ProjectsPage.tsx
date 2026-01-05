// ProjectsPage.tsx
import { useEffect, useState } from 'react';
import type { Project } from '../types/project.types';
import { ProjectService } from '../services/project.service';
import { useNavigate } from 'react-router-dom';
import { ProjectForm } from '../components/ProjectForm';
import { ProjectUpdateForm } from '../components/ProjectUpdateForm';
import { ProjectProgressBar } from '../components/ProjectProgressBar';
import { SearchInput } from '../components/SearchInput';
import { ConfirmPopIn } from '../components/ConfirmPopIn';
import { Edit2, Trash2, FileText, Plus } from 'lucide-react';
import '../styles/projects.css';

export const ProjectsPage = () => {
  const [projects, setProjects] = useState<Project[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [showCreateForm, setShowCreateForm] = useState(false);
  const [editingProjectId, setEditingProjectId] = useState<string | null>(null);
  const [search, setSearch] = useState('');
  const [page, setPage] = useState(1);
  const [totalItems, setTotalItems] = useState(0);
  
  // États pour la popup de confirmation
  const [showConfirm, setShowConfirm] = useState(false);
  const [projectToDelete, setProjectToDelete] = useState<{ id: string; title: string } | null>(null);

  const pageSize = 6;
  const navigate = useNavigate();

  useEffect(() => { 
    loadProjects(page); 
  }, [page]);

  useEffect(() => { 
    loadProjects(1); 
  }, [search]);

  const loadProjects = async (pageNumber: number) => {
    try {
      setLoading(true);
      const data = await ProjectService.getPaged(
        pageNumber, 
        pageSize, 
        search || undefined
      );
      setProjects(data.items);
      setTotalItems(data.totalItems);
      setPage(data.page);
    } catch (err) {
      setError('Failed to load projects');
      console.error(err);
    } finally { 
      setLoading(false); 
    }
  };

  // Gestion de la suppression avec confirmation
  const handleDeleteClick = (projectId: string, projectTitle: string) => {
    setProjectToDelete({ id: projectId, title: projectTitle });
    setShowConfirm(true);
  };

  const handleDeleteConfirm = async () => {
    if (!projectToDelete) return;
    
    try {
      await ProjectService.delete(projectToDelete.id);
      loadProjects(page);
    } catch (err) {
      setError('Failed to delete project');
      console.error(err);
    } finally {
      setShowConfirm(false);
      setProjectToDelete(null);
    }
  };

  const handleDeleteCancel = () => {
    setShowConfirm(false);
    setProjectToDelete(null);
  };

  const handleDetails = (projectId: string) => {
    navigate(`/projects/${projectId}/tasks`);
  };

  const handleCreated = () => { 
    loadProjects(1); 
    setShowCreateForm(false); 
  };

  const handleUpdated = () => { 
    loadProjects(page); 
    setEditingProjectId(null); 
  };

  const totalPages = Math.ceil(totalItems / pageSize);

  return (
    <div className="container mt-4">
      <h2 className="mb-3">My Projects</h2>

      {/* Popup de confirmation */}
      <ConfirmPopIn
        isOpen={showConfirm}
        message={`Are you sure you want to delete "${projectToDelete?.title}"? This action cannot be undone.`}
        confirmLabel="Delete"
        cancelLabel="Cancel"
        onConfirm={handleDeleteConfirm}
        onCancel={handleDeleteCancel}
      />

      <SearchInput 
        value={search} 
        onChange={setSearch} 
        placeholder="Search project by title..." 
      />

      <button
        className="btn btn-success mb-3 d-flex align-items-center"
        onClick={() => setShowCreateForm(!showCreateForm)}
      >
        <Plus size={16} className="me-2" /> 
        {showCreateForm ? 'Cancel' : 'Create Project'}
      </button>

      {showCreateForm && (
        <ProjectForm 
          onCreated={handleCreated} 
          onCancel={() => setShowCreateForm(false)} 
        />
      )}

      {loading && (
        <div className="text-center my-4">
          <div className="spinner-border" role="status">
            <span className="visually-hidden">Loading...</span>
          </div>
        </div>
      )}
      
      {error && (
        <div className="alert alert-danger" role="alert">
          {error}
        </div>
      )}

      {!loading && !error && projects.length === 0 && (
        <div className="alert alert-info" role="alert">
          No projects found. {!showCreateForm && "Create your first project!"}
        </div>
      )}

      <div className="row">
        {projects.map(project => (
          <div className="col-md-4 mb-4" key={project.id}>
            <div className="card shadow-sm project-card h-100">
              <div className="card-body d-flex flex-column">
                {editingProjectId === project.id ? (
                  <ProjectUpdateForm 
                    project={project} 
                    onUpdated={handleUpdated} 
                    onCancel={() => setEditingProjectId(null)} 
                  />
                ) : (
                  <>
                    <h5 className="card-title">{project.title}</h5>
                    <p className="card-text text-muted">
                      {project.description || 'No description provided'}
                    </p>

                    <div className="mt-3">
                      <ProjectProgressBar projectId={project.id} />
                    </div>

                    <div className="mt-auto d-flex justify-content-end gap-2 pt-3">
                      <button 
                        className="btn btn-sm btn-outline-primary" 
                        onClick={() => setEditingProjectId(project.id)}
                        aria-label={`Edit ${project.title}`}
                        title="Edit project"
                      >
                        <Edit2 size={16} />
                      </button>
                      <button 
                        className="btn btn-sm btn-outline-danger" 
                        onClick={() => handleDeleteClick(project.id, project.title)}
                        aria-label={`Delete ${project.title}`}
                        title="Delete project"
                      >
                        <Trash2 size={16} />
                      </button>
                      <button 
                        className="btn btn-sm btn-outline-info" 
                        onClick={() => handleDetails(project.id)}
                        aria-label={`View details for ${project.title}`}
                        title="View project details"
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
        <nav className="d-flex justify-content-between align-items-center mt-4">
          <button 
            className="btn btn-outline-secondary"
            disabled={page <= 1} 
            onClick={() => loadProjects(page - 1)}
          >
            Previous
          </button>
          
          <span className="text-muted">
            Page <strong>{page}</strong> of <strong>{totalPages}</strong>
          </span>
          
          <button 
            className="btn btn-outline-secondary"
            disabled={page >= totalPages} 
            onClick={() => loadProjects(page + 1)}
          >
            Next
          </button>
        </nav>
      )}
    </div>
  );
};

export default ProjectsPage;