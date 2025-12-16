import { apiClient } from './axios';
import type { Task } from '../types/task.types';

export const TaskApi = {
  // GET /api/projects/{projectId}/tasks
  getByProject: async (projectId: string): Promise<Task[]> => {
    const response = await apiClient.get<Task[]>(
      `/projects/${projectId}/tasks`
    );
    return response.data;
  },

  
  markAsCompleted: async (
    projectId: string,
    taskId: string
  ): Promise<void> => {
    await apiClient.patch(
      `/projects/${projectId}/tasks/${taskId}/complete`
    );
  },

  
  delete: async (
    projectId: string,
    taskId: string
  ): Promise<void> => {
    await apiClient.delete(
      `/projects/${projectId}/tasks/${taskId}`
    );
  },

 
  create: async (
    projectId: string,
    payload: { title: string; description?: string }
  ): Promise<Task> => {
    const response = await apiClient.post<Task>(
      `/projects/${projectId}/tasks`,
      payload
    );
    return response.data;
  }
};
