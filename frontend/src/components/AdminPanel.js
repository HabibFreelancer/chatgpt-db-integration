import React, { useEffect, useState } from 'react';
import axios from 'axios';
import '../AdminPanel.css';

function AdminPanel() {
  const [customers, setCustomers] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);

  useEffect(() => {
    async function fetchCustomers() {
      setLoading(true);
      setError(null);

      try {
        const response = await axios.get(`${process.env.REACT_APP_API_URL}/api/customer`);
        setCustomers(response.data);
      } catch (err) {
        setError('Failed to load customers.');
      } finally {
        setLoading(false);
      }
    }

    fetchCustomers();
  }, []);

  return (
    <div className="admin-page-wrapper">
      <div className="admin-container">
        <header className="admin-header">Admin Panel</header>

        <section className="admin-section">
          <h2>Settings</h2>
          <p>Manage your application settings here.</p>
        </section>

        <section className="admin-section customers-section">
          <h2>Customers</h2>

          {loading && <p>Loading customers...</p>}
          {error && <p className="error-message">{error}</p>}

          {!loading && !error && customers.length === 0 && (
            <p>No customers found.</p>
          )}

          {!loading && !error && customers.length > 0 && (
            <div className="table-wrapper">
              <table className="customers-table">
                <thead>
                  <tr>
                    <th>ID</th>
                    <th>First Name</th>
                    <th>Last Name</th>
                    <th>Email</th>
                  </tr>
                </thead>
                <tbody>
                  {customers.map(c => (
                    <tr key={c.customerId}>
                      <td>{c.customerId}</td>
                      <td>{c.firstName}</td>
                      <td>{c.lastName}</td>
                      <td>{c.email}</td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
          )}
        </section>
      </div>
    </div>
  );
}

export default AdminPanel;
