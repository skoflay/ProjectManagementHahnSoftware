import type { Task } from '../types/task.types';
import { TaskApi } from '../api/task.api';

export const TaskService = {
  getByProject: async (projectId: string): Promise<Task[]> => {
    return TaskApi.getByProject(projectId);
  },

  markAsCompleted: async (
    projectId: string,
    taskId: string
  ): Promise<void> => {
    return TaskApi.markAsCompleted(projectId, taskId);
  },

  create: async (
    projectId: string,
    data: Task
  ): Promise<Task> => {
    return TaskApi.create(projectId, data);
  },

  delete: async (
    projectId: string,
    taskId: string
  ): Promise<void> => {
    return TaskApi.delete(projectId, taskId);
  }
};
