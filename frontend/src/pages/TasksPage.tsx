import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { TaskService } from '../services/task.service';
import type { Task } from '../types/task.types';
import { TaskCreateForm } from '../components/TaskCreateForm';
import { TaskUpdateForm } from '../components/TaskUpdateForm';
import { SearchInput } from '../components/SearchInput';
import { Edit2, Trash2, CheckCircle, Circle } from 'lucide-react';
import { ProjectService } from '../services/project.service';
import type { Project } from '../types/project.types';
import '../styles/tasks.css';

type CompletionFilter = 'all' | 'completed' | 'pending';

export const TasksPage = () => {
  const { projectId } = useParams<{ projectId: string }>();

  const [project, setProject] = useState<Project | null>(null);
  const [tasks, setTasks] = useState<Task[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const [showCreateForm, setShowCreateForm] = useState(false);
  const [editingTaskId, setEditingTaskId] = useState<string | null>(null);

  // Pagination
  const [page, setPage] = useState(1);
  const pageSize = 6;
  const [totalItems, setTotalItems] = useState(0);

  // Search & filter
  const [search, setSearch] = useState('');
  const [filter, setFilter] = useState<CompletionFilter>('all');

  const totalPages = Math.ceil(totalItems / pageSize);


  const loadProject = async () => {
    if (!projectId) return;
    try {
      const data = await ProjectService.getById(projectId);
      setProject(data);
    } catch (err) {
      console.error(err);
    }
  };

  
  const loadTasks = async (pageNumber: number = page) => {
    if (!projectId) return;

    try {
      setLoading(true);

      const data = await TaskService.getPaged(
        projectId,
        pageNumber,
        pageSize,
        search || undefined
      );

      let items = data.items || [];

      
      if (filter === 'completed') {
        items = items.filter(t => t.isCompleted);
      }
      if (filter === 'pending') {
        items = items.filter(t => !t.isCompleted);
      }

      setTasks(items);
      setTotalItems(data.totalItems || 0);
      setPage(data.page || 1);
    } catch (err) {
      console.error(err);
      setError('Failed to load tasks');
    } finally {
      setLoading(false);
    }
  };

 
  useEffect(() => { loadProject(); }, [projectId]);
  useEffect(() => { loadTasks(page); }, [page, projectId]);
  useEffect(() => { loadTasks(1); }, [search, filter]);

 
  const handleDelete = async (taskId: string) => {
    if (!confirm('Are you sure?')) return;
    await TaskService.delete(projectId!, taskId);
    loadTasks(page);
  };

  const handleCreated = (task: Task) => {
    setTasks([task, ...tasks]);
    setShowCreateForm(false);
  };

  const handleUpdated = (updated: Task) => {
    setTasks(tasks.map(t => t.id === updated.id ? updated : t));
    setEditingTaskId(null);
  };

  const handleMarkCompleted = async (taskId: string) => {
    if (!projectId) return;
    await TaskService.markAsCompleted(projectId, taskId);
    loadTasks(page);
  };


  return (
    <div className="container mt-4">
      <h2 className="mb-3">
        Tasks for Project {project ? `"${project.title}"` : projectId}
      </h2>

      {/* SEARCH */}
      <SearchInput
        value={search}
        onChange={setSearch}
        placeholder="Search task by title..."
      />

      {/* FILTER BUTTONS */}
      <div className="btn-group mb-3" role="group" aria-label="Task filter">
        <button
          className={`btn btn-outline-primary ${filter === 'all' ? 'active' : ''}`}
          onClick={() => setFilter('all')}
        >
          All
        </button>
        <button
          className={`btn btn-outline-success ${filter === 'completed' ? 'active' : ''}`}
          onClick={() => setFilter('completed')}
        >
          Completed
        </button>
        <button
          className={`btn btn-outline-secondary ${filter === 'pending' ? 'active' : ''}`}
          onClick={() => setFilter('pending')}
        >
          Pending
        </button>
      </div>

      {/* CREATE */}
      <button
        className="btn btn-success mb-3 d-flex align-items-center"
        onClick={() => setShowCreateForm(!showCreateForm)}
      >
        {showCreateForm ? 'Cancel' : <>➕ Create Task</>}
      </button>

      {showCreateForm && projectId && (
        <TaskCreateForm
          projectId={projectId}
          onCreated={handleCreated}
          onCancel={() => setShowCreateForm(false)}
        />
      )}

      {loading && <p>Loading...</p>}
      {error && <p className="text-danger">{error}</p>}

      {/* TASK LIST */}
      <div className="row">
        {tasks.map(task => (
          <div key={task.id} className="col-md-6 mb-3">
            <div className={`card task-card ${task.isCompleted ? 'completed' : ''}`}>
              <div className="card-body">
                {editingTaskId === task.id ? (
                  <TaskUpdateForm
                    projectId={projectId!}
                    task={task}
                    onUpdated={handleUpdated}
                    onCancel={() => setEditingTaskId(null)}
                  />
                ) : (
                  <>
                    <h5>{task.title}</h5>
                    <p>{task.description || 'No description'}</p>
                    <small className="text-muted">
                      Due: {task.dueDate ? task.dueDate.split('T')[0] : 'N/A'}
                    </small>

                    <div className="d-flex gap-2 mt-2">
                      <button
                        className={`btn btn-sm ${task.isCompleted ? 'btn-success' : 'btn-outline-secondary'}`}
                        onClick={() => handleMarkCompleted(task.id)}
                        aria-label={task.isCompleted ? 'Task completed' : 'Mark task as completed'}
                        title={task.isCompleted ? 'Completed' : 'Mark as completed'}
                      >
                        {task.isCompleted ? <CheckCircle /> : <Circle />}
                      </button>

                      <button
                        className="btn btn-sm btn-primary"
                        onClick={() => setEditingTaskId(task.id)}
                        aria-label="Edit task"
                        title="Edit task"
                      >
                        <Edit2 />
                      </button>

                      <button
                        className="btn btn-sm btn-danger"
                        onClick={() => handleDelete(task.id)}
                        aria-label="Delete task"
                        title="Delete task"
                      >
                        <Trash2 />
                      </button>
                    </div>
                  </>
                )}
              </div>
            </div>
          </div>
        ))}
      </div>

      {/* PAGINATION */}
      {totalPages > 1 && (
        <div className="d-flex justify-content-between mt-3">
          <button
            className="btn btn-secondary"
            disabled={page <= 1}
            onClick={() => setPage(page - 1)}
          >
            Previous
          </button>

          <span>Page {page} of {totalPages}</span>

          <button
            className="btn btn-secondary"
            disabled={page >= totalPages}
            onClick={() => setPage(page + 1)}
          >
            Next
          </button>
        </div>
      )}
    </div>
  );
};

export default TasksPage;
