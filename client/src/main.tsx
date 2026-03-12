import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.tsx'

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <div className="flex-1 min-h-0 w-full flex flex-col">
      <App />
    </div>
  </StrictMode>,
)
