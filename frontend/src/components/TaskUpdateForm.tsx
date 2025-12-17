// src/components/TaskUpdateForm.tsx
import { useState } from 'react';
import { TaskService } from '../services/task.service';
import type { Task } from '../types/task.types';

interface Props {
  projectId: string;
  task: Task;
  onUpdated: (task: Task) => void;
  onCancel: () => void;
}

export const TaskUpdateForm = ({ projectId, task, onUpdated, onCancel }: Props) => {
  const [title, setTitle] = useState(task.title);
  const [description, setDescription] = useState(task.description || '');

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    const updatedTask = await TaskService.update(projectId, task.id, { title, description });
    onUpdated(updatedTask);
  };

  return (
    <form onSubmit={handleSubmit} className="mb-2 border p-2 rounded">
      <div className="mb-2">
        <label htmlFor={`title-${task.id}`} className="form-label">Title</label>
        <input 
          id={`title-${task.id}`} 
          type="text" 
          className="form-control" 
          value={title} 
          onChange={e => setTitle(e.target.value)} 
          required 
        />
      </div>

      <div className="mb-2">
        <label htmlFor={`desc-${task.id}`} className="form-label">Description</label>
        <textarea 
          id={`desc-${task.id}`} 
          className="form-control" 
          value={description} 
          onChange={e => setDescription(e.target.value)} 
          required
        />
      </div>

      <button type="submit" className="btn btn-primary me-2">Update</button>
      <button type="button" className="btn btn-secondary" onClick={onCancel}>Cancel</button>
    </form>
  );
};
