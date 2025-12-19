import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import ProjectsPage from './pages/ProjectsPage';
import TasksPage from './pages/TasksPage';
import { LoginPage } from './pages/LoginPage';
import Layout from './components/Layout/Layout';

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/login" element={<LoginPage />} />

        <Route
          path="/projects"
          element={
            <Layout>
              <ProjectsPage />
            </Layout>
          }
        />

        <Route
          path="/projects/:projectId/tasks"
          element={
            <Layout>
              <TasksPage />
            </Layout>
          }
        />
      </Routes>
    </Router>
  );
}

export default App;
