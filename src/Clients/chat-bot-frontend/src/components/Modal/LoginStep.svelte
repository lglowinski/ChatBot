<script lang="ts">
    import { auth } from '../../store/auth.store';
    import { login } from '../../lib/usersApi';

    let email = '';
    let password = '';
    let errorMessage = '';

    async function handleSubmit() {
        try {
            const response = await login(email, password);

            if (response.email) {
                auth.setUserData(response.userId, response.email!);
                auth.setView('verify');
            }

            errorMessage = '';
        } catch (error) {
            errorMessage = error instanceof Error ? error.message : 'Nieoczekiwany błąd';
        }
    }
</script>

<form class="space-y-4" on:submit|preventDefault={handleSubmit}>
    {#if errorMessage}
        <p class="text-red-500">{errorMessage}</p>
    {/if}

    <div class="space-y-2">
        <label for="email" class="text-white">Email</label>
        <input
                id="email"
                type="email"
                bind:value={email}
                placeholder="john.doe@example.com"
                class="w-full p-2 bg-gray-700 rounded text-white"
                required
        />
    </div>

    <div class="space-y-2">
        <label for="password" class="text-white">Password</label>
        <input
                id="password"
                type="password"
                bind:value={password}
                placeholder="Password"
                class="w-full p-2 bg-gray-700 rounded text-white"
                required
        />
    </div>

    <button
            type="submit"
            class="w-full bg-yellow-500 text-black font-bold py-2 px-4 rounded hover:bg-yellow-600"
    >
        Login
    </button>

    <p class="text-center text-gray-400 mt-4">
        Don't have account?
        <button
                type="button"
                class="text-yellow-500 hover:underline ml-1"
                on:click={() => auth.setView('register')}
        >
            Register
        </button>
    </p>
</form>