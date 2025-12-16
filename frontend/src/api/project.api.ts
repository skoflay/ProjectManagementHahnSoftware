import { apiClient } from './axios';
import type { Project } from '../types/project.types';

export const getProjects = async (): Promise<Project[]> => {
  const response = await apiClient.get('/Projects');
  return response.data;
};

export const createProject = async (project: Partial<Project>) => {
  return apiClient.post('/Projects', project);
};


export const updateProject = async (project: Partial<Project>) => {
  return apiClient.put('/Projects', project);
};

export const DeleteProject = async () => {
  return apiClient.delete('/Projects',);
};