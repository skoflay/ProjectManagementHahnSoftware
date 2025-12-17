
import { ProjectApi } from '../api/project.api';
import type { Project } from '../types/project.types';
import type {ProjectProgress} from '../types/projectprogress.types'

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
  }


};
