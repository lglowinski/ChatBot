<script lang="ts">
  import { createEventDispatcher } from "svelte";
  import { X } from "lucide-svelte";
  import { auth } from "../../store/auth.store";
  import Login from "./LoginStep.svelte";
  import Register from "./RegisterStep.svelte";
  import Verify from "./VerifyStep.svelte";

  export let isOpen = false;

  const dispatch = createEventDispatcher();

  function closeModal() {
    dispatch("close");
    if (!$auth.isAuthenticated) {
      auth.reset();
    }
  }


  $: if ($auth.view === 'logout') {
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
          {#if $auth.view === "login"}
            Login
          {:else if $auth.view === "register"}
            Register
          {:else}
            Verify
          {/if}
        </h2>
        <button
                on:click={closeModal}
                class="text-gray-400 hover:text-white"
                aria-label="Zamknij"
        >
          <X size={24} />
        </button>
      </div>

      {#if $auth.view === "login"}
        <Login />
      {:else if $auth.view === "register"}
        <Register />
      {:else}
        <Verify />
      {/if}
    </div>
  </div>
{/if}

<style>
  /* Opcjonalnie: dodatkowe style, jeśli potrzebne */
  .fixed {
    /* Zapewnienie, że modal będzie zawsze na wierzchu */
    z-index: 50;
  }
</style>