import * as projectApi from '../api/project.api';

export const ProjectService = {
  getAll: () => projectApi.getProjects(),
  create: (title: string, description?: string) =>
    projectApi.createProject({ title, description })
};
