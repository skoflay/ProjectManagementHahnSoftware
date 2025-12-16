import type { Task } from '../types/task.types';
import { TaskApi } from '../api/task.api';

export const TaskService = {
  getByProject: async (projectId: string): Promise<Task[]> => {
    return TaskApi.getByProject(projectId);
  },
  markAsCompleted: async (taskId: string): Promise<void> => {
    return TaskApi.markAsCompleted(taskId);
  },
  delete: async (taskId: string): Promise<void> => {
    return TaskApi.delete(taskId);
  }
};
