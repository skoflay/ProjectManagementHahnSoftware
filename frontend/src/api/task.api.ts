import { apiClient } from './axios';
import type { Task } from '../types/task.types';
import type { PagedResult } from '../types/PagedResult';

export const TaskApi = {
  getByProject: async (projectId: string): Promise<Task[]> => {
    const response = await apiClient.get<Task[]>(`/projects/${projectId}/tasks`);
    return response.data;
  },

  markAsCompleted: async (projectId: string, taskId: string): Promise<void> => {
    await apiClient.patch(`/projects/${projectId}/tasks/${taskId}/complete`);
  },

  delete: async (projectId: string, taskId: string): Promise<void> => {
    await apiClient.delete(`/projects/${projectId}/tasks/${taskId}`);
  },

  create: async (
    projectId: string,
    payload: { title: string; description?: string; dueDate?: string; isCompleted?: boolean }
  ): Promise<Task> => {
    const response = await apiClient.post<Task>(
      `/projects/${projectId}/tasks`,
      payload
    );
    return response.data;
  },

  update: async (projectId: string, taskId: string, payload: { title: string; description?: string }): Promise<Task> => {
    const response = await apiClient.patch<Task>(`/projects/${projectId}/tasks/${taskId}`, payload);
    return response.data;
  },

getPaged: async (
  projectId: string,
  page: number,
  pageSize: number,
  search?: string
): Promise<PagedResult<Task>> => {
  const res = await apiClient.get<PagedResult<Task>>(
    `/projects/${projectId}/tasks/paged`,
    {
      params: {
        page,
        pageSize,
        ...(search && { search })
      }
    }
  );
  return res.data;
}


};
