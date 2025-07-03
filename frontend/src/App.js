import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';

import ChatPanel from './components/ChatPanel';
import AdminPanel from './components/AdminPanel';

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<ChatPanel />} />
        <Route path="/admin" element={<AdminPanel />} />
      </Routes>
    </Router>
  );
}

export default App;
