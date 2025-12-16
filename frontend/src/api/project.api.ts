
import { apiClient } from './axios';
import type { Project } from '../types/project.types';

export const ProjectApi = {
  getAll: async (): Promise<Project[]> => {
    const response = await apiClient.get<Project[]>('/Projects');
    return response.data;
  },
  delete: async (projectId: string): Promise<void> => {
    await apiClient.delete(`/Projects/${projectId}`);
  },
  update: async (projectId: string, data: Partial<Project>): Promise<Project> => {
    const response = await apiClient.put<Project>(`/Projects/${projectId}`, data);
    return response.data;
  }
};
