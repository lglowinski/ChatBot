import {writable} from "svelte/store"

export interface User {
    id: string;
    email: string;
}


export interface AuthState {
    user: User | null;
    token: string | null;
    isAuthenticated: boolean;
}

const createAuthStore = () => {
    const {subscribe, set,update} = writable<AuthState>({user:null, token:null, isAuthenticated:false});

    return {
        subscribe,
        login: (user : User, token: string) => update(state => ({user, token, isAuthenticated: true})),
        logout: () => set({user:null, token: null, isAuthenticated: false}),
        setToken: (token: string) => update(state => ({...state, token})),
    }
}

export const auth = createAuthStore();