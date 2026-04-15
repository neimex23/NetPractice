import { Router, Route, Switch, Redirect } from 'wouter';
import { useHashLocation } from 'wouter/use-hash-location';
import Sidebar from './components/Sidebar';
import Toast from './components/Toast';
import { useToast } from './hooks/useToast';
import PaisesPage from './pages/PaisesPage';
import ConfederacionesPage from './pages/ConfederacionesPage';
import DeportesPage from './pages/DeportesPage';

export default function App() {
  const { toast, showToast } = useToast();

  return (
    <Router hook={useHashLocation}>
      <div className="flex min-h-screen bg-stone-100 font-sans">
        <Sidebar />

        <main className="flex-1 p-8 overflow-y-auto">
          <Switch>
            <Route path="/paises">
              <PaisesPage showToast={showToast} />
            </Route>
            <Route path="/confederaciones">
              <ConfederacionesPage showToast={showToast} />
            </Route>
            <Route path="/deportes">
              <DeportesPage showToast={showToast} />
            </Route>
            <Route>
              <Redirect to="/paises" />
            </Route>
          </Switch>
        </main>

        <Toast toast={toast} />
      </div>
    </Router>
  );
}
