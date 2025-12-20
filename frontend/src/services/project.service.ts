
import { ProjectApi } from '../api/project.api';
import type { Project } from '../types/project.types';
import type {ProjectProgress} from '../types/projectprogress.types'
import type { PagedResult } from '../types/PagedResult';
export const ProjectService = {
  getAll: async (): Promise<Project[]> => {
    return ProjectApi.getAll();
  },
  delete: async (projectId: string): Promise<void> => {
    return ProjectApi.delete(projectId);
  },
  update: async (projectId: string, data: Partial<Project>): Promise<Project> => {
    return ProjectApi.update(projectId, data);
  },
  create: async (data: Partial<Project>): Promise<Project> => {
    return ProjectApi.create(data);
  },

   getProgress: async (projectId: string): Promise<ProjectProgress> => {
    return ProjectApi.getProgress(projectId);
  },
  
   getPaged: async (
  page: number,
  pageSize: number,
  search?: string
): Promise<PagedResult<Project>> => {
  return ProjectApi.getPaged(page, pageSize, search);
},

getById: async (projectId: string): Promise<Project> => {
  return await ProjectApi.getById(projectId); 
},




};
