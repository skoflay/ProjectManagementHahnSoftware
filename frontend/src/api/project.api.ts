
import { apiClient } from './axios';
import type { Project } from '../types/project.types';
import type {ProjectProgress} from '../types/projectprogress.types';
import type { PagedResult } from '../types/PagedResult';



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
  },

  getPaged: async (
  page: number,
  pageSize: number,
  search?: string
): Promise<PagedResult<Project>> => {
  const res = await apiClient.get<PagedResult<Project>>(
    `/projects`,
    {
      params: {
        page,
        pageSize,
        search
      }
    }
  );
  return res.data;
},
 getById: async (projectId: string): Promise<Project> => {
    const res = await apiClient.get<Project>(`/projects/${projectId}`);
    return res.data;
  },



};
