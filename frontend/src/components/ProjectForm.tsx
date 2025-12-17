import { useState } from 'react';
import type { Project } from '../types/project.types';
import { ProjectService } from '../services/project.service';

interface ProjectFormProps {
  onCreated: (project: Project) => void;
  onCancel: () => void;
}

export const ProjectForm = ({ onCreated, onCancel }: ProjectFormProps) => {
  const [title, setTitle] = useState('');
  const [description, setDescription] = useState('');

  const handleSubmit = async () => {
    if (!title.trim()) return alert('Title is required');
    const newProject = await ProjectService.create({ title, description });
    onCreated(newProject);
  };

  return (
    <div className="card mb-3 p-3">
      <input
        type="text"
        placeholder="Title"
        className="form-control mb-2"
        value={title}
        onChange={e => setTitle(e.target.value)}
      />
      <textarea
        placeholder="Description"
        className="form-control mb-2"
        value={description}
        onChange={e => setDescription(e.target.value)}
      />
      <button className="btn btn-primary me-2" onClick={handleSubmit}>Create</button>
      <button className="btn btn-secondary" onClick={onCancel}>Cancel</button>
    </div>
  );
};
