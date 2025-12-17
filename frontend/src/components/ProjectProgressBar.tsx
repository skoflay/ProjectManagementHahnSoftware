import { useEffect, useState } from 'react';
import { TaskService } from '../services/task.service';
import type { Task } from '../types/task.types';

interface Props {
  projectId: string;
}

export const ProjectProgressBar = ({ projectId }: Props) => {
  const [tasks, setTasks] = useState<Task[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    loadTasks();
  }, [projectId]);

  const loadTasks = async () => {
    try {
      const data = await TaskService.getByProject(projectId);
      setTasks(data);
    } finally {
      setLoading(false);
    }
  };

  if (loading) return null;

  const total = tasks.length;
  const completed = tasks.filter(t => t.isCompleted).length;

  const isCompleted = total > 0 && completed === total;

  return (
    <div className="mt-2">
      <div className="d-flex justify-content-between mb-1">
        <small>
          {isCompleted
            ? 'Project completed'
            : total === 0
              ? 'No tasks'
              : 'In progress'}
        </small>
        <small>
          {completed}/{total}
        </small>
      </div>

      <progress
        className="w-100"
        value={completed}
        max={total === 0 ? 1 : total}
        aria-label="Project progress"
      />
    </div>
  );
};
