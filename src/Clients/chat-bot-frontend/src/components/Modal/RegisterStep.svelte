<script lang="ts">
    import { auth } from '../../store/auth.store';
    import { register } from '../../lib/usersApi';
    import {error} from "@sveltejs/kit";

    let email = '';
    let password = '';
    let confirmPassword = '';
    let errorMessage = '';

    async function handleSubmit() {
        if (password !== confirmPassword) {
            errorMessage = 'Passwords do not match';
            return;
        }

        try {
            const response = await register(email, password);

            if (response.secretKey && response.qrCodeUri) {
                auth.setUserData(response.userId, response.email!);
                auth.setQrData(response.qrCodeUri, response.secretKey);
                auth.setView('verify');
            }

            errorMessage = '';
        } catch (error) {
            errorMessage = error instanceof Error ? JSON.parse(error.message).detail : 'Unexpected error';
        }
    }
</script>

<form class="space-y-4" on:submit|preventDefault={handleSubmit}>
    {#if errorMessage}
        <p class="text-red-500">{errorMessage}</p>
    {/if}

    <div class="space-y-2">
        <label for="register-email" class="text-white">Email</label>
        <input
                id="register-email"
                type="email"
                bind:value={email}
                placeholder="john.doe@example.com"
                class="w-full p-2 bg-gray-700 rounded text-white"
                required
        />
    </div>

    <div class="space-y-2">
        <label for="register-password" class="text-white">Password</label>
        <input
                id="register-password"
                type="password"
                bind:value={password}
                placeholder="Password"
                class="w-full p-2 bg-gray-700 rounded text-white"
                required
        />
    </div>

    <div class="space-y-2">
        <label for="confirm-password" class="text-white">Confirm password</label>
        <input
                id="confirm-password"
                type="password"
                bind:value={confirmPassword}
                placeholder="Password"
                class="w-full p-2 bg-gray-700 rounded text-white"
                required
        />
    </div>

    <button
            type="submit"
            class="w-full bg-yellow-500 text-black font-bold py-2 px-4 rounded hover:bg-yellow-600"
    >
        Register
    </button>

    <p class="text-center text-gray-400 mt-4">
        Already got an account?
        <button
                type="button"
                class="text-yellow-500 hover:underline ml-1"
                on:click={() => auth.setView('login')}
        >
            Login
        </button>
    </p>
</form>