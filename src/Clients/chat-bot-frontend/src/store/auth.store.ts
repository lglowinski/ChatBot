import { writable } from "svelte/store";

export interface AuthState {
    email: string | null;
    userId: string | null;
    token: string | null;
    isAuthenticated: boolean;
    view: 'login' | 'register' | 'verify' | "logout";
    qrCodeUri?: string;
    secretKey?: string;
}

const initialState: AuthState = {
    email: null,
    userId: null,
    token: null,
    isAuthenticated: false,
    view: 'login'
};

const createAuthStore = () => {
    const { subscribe, set, update } = writable<AuthState>(initialState);

    return {
        subscribe,


        setUserData: (userId: string, email: string) =>
            update(state => ({
                ...state,
                userId,
                email
            })),


        setAuthenticated: (email: string, token: string) =>{
            update(state => ({
                ...state,
                email,
                token,
                isAuthenticated: true,
                view: 'logout',
                qrCodeUri: undefined,
                secretKey: undefined
            }));

            localStorage.s
        },


        logout: () => set(initialState),

        setView: (view: AuthState['view']) =>
            update(state => ({
                ...state,
                view
            })),

        setQrData: (qrCodeUri: string, secretKey: string) =>
            update(state => ({
                ...state,
                qrCodeUri,
                secretKey
            })),

        reset: () => set(initialState)
    };
};

export const auth = createAuthStore();