
import { ProjectApi } from '../api/project.api';
import type { Project } from '../types/project.types';

export const ProjectService = {
  getAll: async (): Promise<Project[]> => {
    return ProjectApi.getAll();
  },
  delete: async (projectId: string): Promise<void> => {
    return ProjectApi.delete(projectId);
  },
  update: async (projectId: string, data: Partial<Project>): Promise<Project> => {
    return ProjectApi.update(projectId, data);
  }
};
