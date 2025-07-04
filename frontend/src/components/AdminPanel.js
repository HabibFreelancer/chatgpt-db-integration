import React, { useEffect, useState } from 'react';
import axios from 'axios';
import '../AdminPanel.css';

function AdminPanel() {
  const [customers, setCustomers] = useState([]);
  const [products, setProducts] = useState([]);
  const [orders, setOrders] = useState([]);
  const [loading, setLoading] = useState({
    customers: false,
    products: false,
    orders: false
  });
  const [error, setError] = useState({
    customers: null,
    products: null,
    orders: null
  });

  useEffect(() => {
    async function fetchCustomers() {
      setLoading(prev => ({ ...prev, customers: true }));
      setError(prev => ({ ...prev, customers: null }));

      try {
        const response = await axios.get(`${process.env.REACT_APP_API_URL}/api/customer`);
        setCustomers(response.data);
      } catch (err) {
        setError(prev => ({ ...prev, customers: 'Failed to load customers.' }));
      } finally {
        setLoading(prev => ({ ...prev, customers: false }));
      }
    }

    async function fetchProducts() {
      setLoading(prev => ({ ...prev, products: true }));
      setError(prev => ({ ...prev, products: null }));

      try {
        const response = await axios.get(`${process.env.REACT_APP_API_URL}/api/product`);
        setProducts(response.data);
      } catch (err) {
        setError(prev => ({ ...prev, products: 'Failed to load products.' }));
      } finally {
        setLoading(prev => ({ ...prev, products: false }));
      }
    }

    async function fetchOrders() {
      setLoading(prev => ({ ...prev, orders: true }));
      setError(prev => ({ ...prev, orders: null }));

      try {
        const response = await axios.get(`${process.env.REACT_APP_API_URL}/api/order`);
        setOrders(response.data);
      } catch (err) {
        setError(prev => ({ ...prev, orders: 'Failed to load orders.' }));
      } finally {
        setLoading(prev => ({ ...prev, orders: false }));
      }
    }

    fetchCustomers();
    fetchProducts();
    fetchOrders();
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

          {loading.customers && <p>Loading customers...</p>}
          {error.customers && <p className="error-message">{error.customers}</p>}

          {!loading.customers && !error.customers && customers.length === 0 && (
            <p>No customers found.</p>
          )}

          {!loading.customers && !error.customers && customers.length > 0 && (
            <div className="table-wrapper">
              <table className="data-table">
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

        <section className="admin-section products-section">
          <h2>Products</h2>

          {loading.products && <p>Loading products...</p>}
          {error.products && <p className="error-message">{error.products}</p>}

          {!loading.products && !error.products && products.length === 0 && (
            <p>No products found.</p>
          )}

          {!loading.products && !error.products && products.length > 0 && (
            <div className="table-wrapper">
              <table className="data-table">
                <thead>
                  <tr>
                    <th>ID</th>
                    <th>Name</th>
                    <th>Price</th>
                  </tr>
                </thead>
                <tbody>
                  {products.map(p => (
                    <tr key={p.productId}>
                      <td>{p.productId}</td>
                      <td>{p.name}</td>
                      <td>${p.price.toFixed(2)}</td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
          )}
        </section>

        <section className="admin-section orders-section">
          <h2>Orders</h2>

          {loading.orders && <p>Loading orders...</p>}
          {error.orders && <p className="error-message">{error.orders}</p>}

          {!loading.orders && !error.orders && orders.length === 0 && (
            <p>No orders found.</p>
          )}

          {!loading.orders && !error.orders && orders.length > 0 && (
            <div className="table-wrapper">
              <table className="data-table">
                <thead>
                  <tr>
                    <th>ID</th>
                    <th>Customer ID</th>
                    <th>Product ID</th>
                    <th>Quantity</th>
                    <th>Order Date</th>
                  </tr>
                </thead>
                <tbody>
                  {orders.map(o => (
                    <tr key={o.orderId}>
                      <td>{o.orderId}</td>
                      <td>{o.customerId}</td>
                      <td>{o.productId}</td>
                      <td>{o.quantity}</td>
                      <td>{new Date(o.orderDate).toLocaleDateString()}</td>
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
