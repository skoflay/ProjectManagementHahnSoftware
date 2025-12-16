import { apiClient } from './axios';
import type { Task } from '../types/task.types';

export const TaskApi = {
  getByProject: async (projectId: string): Promise<Task[]> => {
    const response = await apiClient.get<Task[]>(`/Tasks`, {
      params: { projectId }
    });
    return response.data;
  },
  markAsCompleted: async (taskId: string): Promise<void> => {
    await apiClient.patch(`/Tasks`, null, { params: { taskId } });
  },
  delete: async (taskId: string): Promise<void> => {
    await apiClient.delete(`/Tasks`, { params: { taskId } });
  }
};
