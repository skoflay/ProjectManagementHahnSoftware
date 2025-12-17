
import { apiClient } from './axios';
import type { Project } from '../types/project.types';
import type {ProjectProgress} from '../types/projectprogress.types';

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
  },

  create: async (data: Partial<Project>): Promise<Project> => {
    const response = await apiClient.post<Project>(`/Projects/`, data);
    return response.data;
  },

  getProgress: async (projectId: string): Promise<ProjectProgress> => {
    const res = await apiClient.get<ProjectProgress>(
      `/projects/${projectId}/progress`
    );
    return res.data;
  }
};
