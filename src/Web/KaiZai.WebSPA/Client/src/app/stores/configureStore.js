import { configureStore } from '@reduxjs/toolkit';
import { apiSlice } from '../api/apiSlice.js';
import incomesDataViewSettingsReducer from '../../features/incomes/incomesSlice.js';

export const store = configureStore({
    reducer: {
        [apiSlice.reducerPath]: apiSlice.reducer,
        incomesDataViewSettings: incomesDataViewSettingsReducer
    },
    middleware: getDefaultMiddleware =>
        getDefaultMiddleware().concat(apiSlice.middleware),
    devTools: true
})