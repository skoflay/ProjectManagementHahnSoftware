import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

export default defineConfig({
  plugins: [react()],
  server: {
    port: 3000,
    host: true,  // Listen on all addresses
    strictPort: true,
    watch: {
      usePolling: true  // For Docker hot reload
    }
  },
  define: {
    'process.env': process.env
  }
})