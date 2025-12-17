// src/pages/TasksPage.tsx
import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { TaskService } from '../services/task.service';
import type { Task } from '../types/task.types';
import { TaskCreateForm } from '../components/TaskCreateForm';
import { TaskUpdateForm } from '../components/TaskUpdateForm';

export const TasksPage = () => {
  const { projectId } = useParams<{ projectId: string }>();
  const [tasks, setTasks] = useState<Task[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [showCreateForm, setShowCreateForm] = useState(false);
  const [editingTaskId, setEditingTaskId] = useState<string | null>(null);

  useEffect(() => {
    if (projectId) loadTasks(projectId);
  }, [projectId]);

  const loadTasks = async (projectId: string) => {
    try {
      setLoading(true);
      const data = await TaskService.getByProject(projectId);
      setTasks(data);
    } catch (err) {
      console.error(err);
      setError('Failed to load tasks');
    } finally {
      setLoading(false);
    }
  };

  const handleDelete = async (taskId: string) => {
    if (!confirm('Are you sure?')) return;
    await TaskService.delete(projectId!, taskId);
    setTasks(tasks.filter(t => t.id !== taskId));
  };

  const handleCreated = (task: Task) => {
    setTasks([...tasks, task]);
    setShowCreateForm(false);
  };

  const handleUpdated = (updated: Task) => {
    setTasks(tasks.map(t => t.id === updated.id ? updated : t));
    setEditingTaskId(null);
  };

  const handleMarkCompleted = async (taskId: string) => {
  if (!projectId) return;
  try {
    await TaskService.markAsCompleted(projectId, taskId);
    
    setTasks(prev =>
      prev.map(t =>
        t.id === taskId ? { ...t, isCompleted: true } : t
      )
    );
  } catch (err) {
    console.error(err);
    setError('Failed to mark task as completed');
  }
};


  return (
    <div className="container mt-4">
      <h2 className="mb-4">Tasks for Project {projectId}</h2>

      <button className="btn btn-success mb-3" onClick={() => setShowCreateForm(!showCreateForm)}>
        {showCreateForm ? 'Cancel' : '➕ Create Task'}
      </button>

      {showCreateForm && projectId && (
        <TaskCreateForm projectId={projectId} onCreated={handleCreated} onCancel={() => setShowCreateForm(false)} />
      )}

      {loading && <p>Loading...</p>}
      {error && <p className="text-danger">{error}</p>}

      <ul className="list-group">
        {tasks.map(task => (
          <li key={task.id} className="list-group-item d-flex flex-column">
            {editingTaskId === task.id ? (
              <TaskUpdateForm 
                projectId={projectId!} 
                task={task} 
                onUpdated={handleUpdated} 
                onCancel={() => setEditingTaskId(null)} 
              />
            ) : (
             <div className="d-flex justify-content-between align-items-center">
  <div>
    <strong>{task.title}</strong>
    <div>{task.description}</div>
    <div>Due: {task.dueDate ? task.dueDate.split('T')[0] : new Date().toISOString().split('T')[0]}</div>
    <div>
      {task.isCompleted ? (
        <span className="badge bg-success">Done</span>
      ) : (
        <button 
          className="btn btn-sm btn-danger"
          onClick={() => handleMarkCompleted(task.id)}
        >
          Not completed
        </button>
      )}
    </div>
  </div>
  <div>
    <button className="btn btn-sm btn-primary me-2" onClick={() => setEditingTaskId(task.id)}>✏️</button>
    <button className="btn btn-sm btn-danger" onClick={() => handleDelete(task.id)}>🗑️</button>
  </div>
</div>

            )}
          </li>
        ))}
      </ul>
    </div>
  );
};

export default TasksPage