import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { TaskService } from '../services/task.service';
import type { Task } from '../types/task.types';

const TasksPage = () => {
  const { projectId } = useParams<{ projectId: string }>();
  const [tasks, setTasks] = useState<Task[]>([]);
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

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

  return (
    <div className="container mt-4">
      <h2 className="mb-4">Tasks for Project {projectId}</h2>

      {loading && <p>Loading...</p>}
      {error && <p className="text-danger">{error}</p>}

      {!loading && !error && tasks.length === 0 && <p>No tasks found</p>}

      <ul className="list-group">
        {tasks.map(task => {
        console.log('TASK:', task);
        return (
            <li key={task.id}>
            {task.title}
            </li>
        );
        })}

      </ul>
    </div>
  );
};

export default TasksPage;
