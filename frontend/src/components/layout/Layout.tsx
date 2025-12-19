import { Navbar } from './Navbar';
import { Footer } from './Footer';
import "../../styles/layout.css";

export const Layout = ({ children }: { children: React.ReactNode }) => {
  return (
    <div className="app-container">
      <Navbar />
      <main className="content">{children}</main>
      <Footer />
    </div>
  );
};

export default Layout