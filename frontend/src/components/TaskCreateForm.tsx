// src/components/TaskCreateForm.tsx
import { useState } from 'react';
import { TaskService } from '../services/task.service';
import type { Task } from '../types/task.types';

interface Props {
  projectId: string;
  onCreated: (task: Task) => void;
  onCancel: () => void;
}

export const TaskCreateForm = ({ projectId, onCreated, onCancel }: Props) => {
  const [title, setTitle] = useState('');
  const [description, setDescription] = useState('');
  const [dueDate, setDueDate] = useState<string>(''); // new optional field

  const handleSubmit = async (e: React.FormEvent) => {
  e.preventDefault();

  const newTask = await TaskService.create(projectId, {
    title,
    description,
    dueDate: dueDate ? new Date(dueDate).toISOString() : new Date().toISOString(),
    isCompleted: false, 
  });

  onCreated(newTask);
};


  return (
    <form onSubmit={handleSubmit} className="mb-3 border p-3 rounded">
      <div className="mb-2">
        <label htmlFor="title" className="form-label">Title</label>
        <input 
          id="title" 
          type="text" 
          className="form-control" 
          value={title} 
          onChange={e => setTitle(e.target.value)} 
          required 
        />
      </div>

      <div className="mb-2">
        <label htmlFor="description" className="form-label">Description</label>
        <textarea 
          id="description" 
          className="form-control" 
          value={description} 
          onChange={e => setDescription(e.target.value)} 
          required
        />
      </div>

      <div className="mb-2">
        <label htmlFor="dueDate" className="form-label">Due Date (optional)</label>
        <input
          id="dueDate"
          type="datetime-local"
          className="form-control"
          value={dueDate}
          onChange={e => setDueDate(e.target.value)}
        />
        <small className="text-muted">If left empty, the task will have the current date/time</small>
      </div>

      <button type="submit" className="btn btn-success me-2">Create</button>
      <button type="button" className="btn btn-secondary" onClick={onCancel}>Cancel</button>
    </form>
  );
};
