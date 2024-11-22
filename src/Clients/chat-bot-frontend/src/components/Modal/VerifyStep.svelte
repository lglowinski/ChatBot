<script lang="ts">
    import { auth } from '../../store/auth.store';
    import { verify } from '../../lib/usersApi';
    import QrCode from "../QR/QrCode.svelte";

    let verificationCode = '';
    let errorMessage = '';

    async function handleSubmit() {
        try {
            if (!$auth.email) {
                throw new Error('No email address');
            }

            const response = await verify($auth.email, verificationCode);

            if (response.token && response.userEmail) {
                console.log("user authenticated")
                auth.setAuthenticated(response.userEmail, response.token);
            }

            errorMessage = '';
        } catch (error) {
            errorMessage = error instanceof Error ? error.message : 'Unexpected error';
        }
    }
</script>

<div class="space-y-4">
    {#if errorMessage}
        <p class="text-red-500">{errorMessage}</p>
    {/if}

    {#if $auth.qrCodeUri}
        <div class="space-y-4">
            <p class="text-white text-center">
                Scan this QR code in your authentication app:
            </p>
            <div class="justify-center items-center w-full flex">
            <QrCode
                    value={$auth.qrCodeUri}
            /></div>

            {#if $auth.secretKey}
                <p class="text-white text-center text-sm">
                    Or enter code manually:
                    <span class="font-mono bg-gray-700 px-2 py-1 rounded">
                        {$auth.secretKey}
                    </span>
                </p>
            {/if}
        </div>
    {/if}

    <form on:submit|preventDefault={handleSubmit} class="space-y-4">
        <div class="space-y-2">
            <label for="verification-code" class="text-white">
                Verification code
            </label>
            <input
                    id="verification-code"
                    type="text"
                    bind:value={verificationCode}
                    placeholder="000 000"
                    class="w-full p-2 bg-gray-700 rounded text-white text-center tracking-wider"
                    pattern="[0-9]*"
                    inputmode="numeric"
                    required
            />
        </div>

        <button
                type="submit"
                class="w-full bg-yellow-500 text-black font-bold py-2 px-4 rounded hover:bg-yellow-600"
        >
            Verify
        </button>
    </form>
</div>