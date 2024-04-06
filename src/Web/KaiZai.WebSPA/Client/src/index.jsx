import ReactDOM from 'react-dom/client'
import './index.css'
import 'semantic-ui-less/semantic.less'
import { router } from './app/router/Routes';
import { Provider } from 'react-redux';
import { RouterProvider } from 'react-router-dom';
import { store } from  './app/stores/configureStore'


ReactDOM.createRoot(document.getElementById('root')).render(
  <Provider store={store}>
        <RouterProvider router={router} />
      </Provider>
)
