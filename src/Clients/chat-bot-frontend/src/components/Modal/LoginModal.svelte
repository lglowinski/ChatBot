<script lang="ts">
  import { createEventDispatcher, getContext } from "svelte";
  import { X, Github, Linkedin } from "lucide-svelte";
  import type { RegisterUserResponse } from "../../lib/usersApi";

  export let isOpen = false;

  let isLogin = true;
  let registrationStep = 1; // 1: form, 2: QR verification
  let registrationData: RegisterUserResponse | null = null;
  let email = "";
  let password = "";
  let confirmPassword = "";
  let errorMessage = "";

  const dispatch = createEventDispatcher();
  // const { register } = getContext<{
  //   register: (
  //     email: string,
  //     password: string
  //   ) => Promise<{
  //     success: boolean;
  //     data?: RegisterUserResponse;
  //     error?: string;
  //   }>;
  // }>("page");

  function closeModal() {
    dispatch("close");
    resetForm();
  }

  function toggleMode() {
    isLogin = !isLogin;
    resetForm();
  }

  function resetForm() {
    registrationStep = 1;
    registrationData = null;
    email = "";
    password = "";
    confirmPassword = "";
    errorMessage = "";
  }

  async function handleSubmit() {
    if (isLogin) {
      // Handle login logic (not implemented in this example)
      console.log("Login not implemented");
    } else {
      if (password !== confirmPassword) {
        errorMessage = "Hasła nie są zgodne";
        return;
      }

      try {
        const result = await register(email, password);
        if (result.success && result.data) {
          registrationData = result.data;
          registrationStep = 2;
          errorMessage = "";
        } else {
          errorMessage =
            "Błąd rejestracji: " + (result.error || "Nieznany błąd");
        }
      } catch (error) {
        errorMessage = "Nieoczekiwany błąd: " + error.message;
      }
    }
  }

  function handleQRVerification() {
    // Here you would typically verify the QR code
    // For this example, we'll just close the modal
    closeModal();
  }
</script>

{#if isOpen}
  <div
    class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center p-4"
  >
    <div class="bg-gray-900 rounded-lg p-6 w-full max-w-md">
      <div class="flex justify-between items-center mb-4">
        <h2 class="text-xl font-bold text-white">
          {#if isLogin}
            Zaloguj się
          {:else if registrationStep === 1}
            Zarejestruj się
          {:else}
            Weryfikacja QR
          {/if}
        </h2>
        <button on:click={closeModal} class="text-gray-400 hover:text-white">
          <X size={24} />
        </button>
      </div>

      {#if errorMessage}
        <p class="text-red-500 mb-4">{errorMessage}</p>
      {/if}

      {#if isLogin || registrationStep === 1}
        <form class="space-y-4" on:submit|preventDefault={handleSubmit}>
          <input
            type="email"
            bind:value={email}
            placeholder="Email"
            class="w-full p-2 bg-gray-800 rounded text-white"
            required
          />
          <input
            type="password"
            bind:value={password}
            placeholder="Hasło"
            class="w-full p-2 bg-gray-800 rounded text-white"
            required
          />
          {#if !isLogin}
            <input
              type="password"
              bind:value={confirmPassword}
              placeholder="Powtórz hasło"
              class="w-full p-2 bg-gray-800 rounded text-white"
              required
            />
          {/if}
          <button
            type="submit"
            class="w-full bg-yellow-500 text-black font-bold py-2 px-4 rounded hover:bg-yellow-600"
          >
            {isLogin ? "Zaloguj" : "Zarejestruj"}
          </button>
        </form>

        <div class="mt-4">
          <p class="text-center text-gray-400 my-2">lub</p>
          <div class="flex space-x-2">
            <button
              class="flex-1 bg-blue-600 text-white py-2 px-4 rounded flex items-center justify-center hover:bg-blue-700"
            >
              <Linkedin size={20} class="mr-2" />
              LinkedIn
            </button>
            <button
              class="flex-1 bg-gray-700 text-white py-2 px-4 rounded flex items-center justify-center hover:bg-gray-600"
            >
              <Github size={20} class="mr-2" />
              GitHub
            </button>
          </div>
        </div>

        <p class="text-center text-gray-400 mt-4">
          {isLogin ? "Nie masz konta?" : "Masz już konto?"}
          <button
            class="text-yellow-500 hover:underline ml-1"
            on:click={toggleMode}
          >
            {isLogin ? "Zarejestruj się" : "Zaloguj się"}
          </button>
        </p>
      {:else}
        <div class="space-y-4">
          <p class="text-white">
            Zeskanuj ten kod QR w swojej aplikacji autentykacyjnej:
          </p>
          <img
            src={registrationData?.qrCodeUri}
            alt="QR Code"
            class="mx-auto"
          />
          <p class="text-white">
            Lub wprowadź ten klucz ręcznie: {registrationData?.secretKey}
          </p>
          <input
            type="text"
            placeholder="Wprowadź kod weryfikacyjny"
            class="w-full p-2 bg-gray-800 rounded text-white"
          />
          <button
            on:click={handleQRVerification}
            class="w-full bg-yellow-500 text-black font-bold py-2 px-4 rounded hover:bg-yellow-600"
          >
            Zweryfikuj
          </button>
        </div>
      {/if}
    </div>
  </div>
{/if}
