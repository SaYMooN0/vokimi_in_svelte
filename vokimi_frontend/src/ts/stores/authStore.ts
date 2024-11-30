import { writable, get } from 'svelte/store';

export class AuthStoreData {
    constructor(
        public readonly Email?: string,
        public readonly Username?: string,
        public readonly UserId?: string,
        public readonly lastFetched: Date = new Date()
    ) { }

    public isAuthenticated(): boolean {
        return this.Username !== undefined &&
            this.Username !== "" &&
            this.UserId !== undefined &&
            this.UserId !== "" &&
            this.Email !== undefined &&
            this.Email !== "";
    }
}

const authData = writable<AuthStoreData | null>(null);

async function fetchAuthData(): Promise<void> {
    try {
        const response = await fetch("/api/auth/ping");

        if (response.status === 200) {
            const data = await response.json();
            const newAuthData = new AuthStoreData(
                data.email,
                data.username,
                data.userId,
                new Date()
            );
            authData.set(newAuthData);
        } else {
            authData.set(null);
        }
    } catch (error) {
        console.error('Failed to fetch auth data', error);
        authData.set(null);
    }
}

async function getAuthData(): Promise<AuthStoreData | null> {
    let currentData = get(authData);

    const now = new Date();
    const two_minutes = 2 * 60 * 1000;

    if (!currentData || (now.getTime() - currentData.lastFetched.getTime() > two_minutes)) {
        await fetchAuthData();
        currentData = get(authData);
    }

    return currentData;
}
export async function getAuthDataForced(): Promise<AuthStoreData | null> {
    await fetchAuthData();
    return get(authData);
}

export default getAuthData;
