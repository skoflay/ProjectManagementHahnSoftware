import '../../styles/footer.css';

export const Footer = () => {
  return (
    <footer className="footer">
      <p>
        © {new Date().getFullYear()} ProjectFlow — Built with ❤️ by Souhil
      </p>
    </footer>
  );
};
