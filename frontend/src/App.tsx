import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import ProjectsPage from './pages/ProjectsPage';
import TasksPage from './pages/TasksPage';

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<ProjectsPage />} />
        <Route path="/projects/:projectId/tasks" element={<TasksPage />} />
      </Routes>
    </Router>
  );
}

export default App;
