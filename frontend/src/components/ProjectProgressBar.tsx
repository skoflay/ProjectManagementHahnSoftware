import { useEffect, useState } from 'react';
import { ProjectService } from '../services/project.service';
import type {ProjectProgress} from '../types/projectprogress.types'

interface Props {
  projectId: string;
}

export const ProjectProgressBar = ({ projectId }: Props) => {
  const [progress, setProgress] = useState<ProjectProgress | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    loadProgress();
  }, [projectId]);

  const loadProgress = async () => {
    try {
      const data = await ProjectService.getProgress(projectId);
      setProgress(data);
    } finally {
      setLoading(false);
    }
  };

  if (loading || !progress) return null;

  const {
    totalTasks,
    completedTasks,
    progressPercentage,
    isCompleted
  } = progress;

  const statusLabel =
    totalTasks === 0
      ? 'No tasks'
      : isCompleted
        ? 'Project completed'
        : 'In progress';

  return (
    <div className="mt-2">
      <div className="d-flex justify-content-between mb-1">
        <small>{statusLabel}</small>
        <small>
          {completedTasks}/{totalTasks}
        </small>
      </div>

      <progress
        className="w-100"
        value={progressPercentage}
        max={100}
        aria-label="Project progress"
      />
    </div>
  );
};