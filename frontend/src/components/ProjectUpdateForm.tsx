import { useState } from 'react';
import type { Project } from '../types/project.types';
import { ProjectService } from '../services/project.service';

interface ProjectUpdateFormProps {
  project: Project;
  onUpdated: (updated: Project) => void;
  onCancel: () => void;
}

export const ProjectUpdateForm = ({ project, onUpdated, onCancel }: ProjectUpdateFormProps) => {
  const [title, setTitle] = useState(project.title);
  const [description, setDescription] = useState(project.description || '');
  const [loading, setLoading] = useState(false);

  const handleSubmit = async () => {
    if (!title.trim()) return alert('Title is required');
    try {
      setLoading(true);
      const updated = await ProjectService.update(project.id, { title, description });
      onUpdated(updated);
    } catch (err) {
      console.error(err);
      alert('Failed to update project');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="card mb-3 p-3">
      <input aria-label="here" type="text"
        className="form-control mb-2"
        value={title}
        onChange={e => setTitle(e.target.value)}
      />
      <textarea aria-label="here2"
        className="form-control mb-2"
        value={description}
        onChange={e => setDescription(e.target.value)}
      />
      <button className="btn btn-primary me-2" onClick={handleSubmit} disabled={loading}>
        {loading ? 'Updating...' : 'Update'}
      </button>
      <button className="btn btn-secondary" onClick={onCancel}>Cancel</button>
    </div>
  );
};
