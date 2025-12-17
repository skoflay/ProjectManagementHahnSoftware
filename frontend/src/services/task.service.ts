import type { Task } from '../types/task.types';
import { TaskApi } from '../api/task.api';

export const TaskService = {
  getByProject: async (projectId: string): Promise<Task[]> => 
    TaskApi.getByProject(projectId),

  markAsCompleted: async (projectId: string, taskId: string): Promise<void> =>
     TaskApi.markAsCompleted(projectId, taskId),

  create: async (
    projectId: string,
    data: { title: string; description?: string; dueDate?: string; isCompleted?: boolean }
  ): Promise<Task> => {
    return TaskApi.create(projectId, data);
  },


  delete: async (projectId: string, taskId: string): Promise<void> => 
    TaskApi.delete(projectId, taskId),

  update: async (projectId: string, taskId: string, data: { title: string; description?: string }): Promise<Task> =>
     TaskApi.update(projectId, taskId, data)
};
