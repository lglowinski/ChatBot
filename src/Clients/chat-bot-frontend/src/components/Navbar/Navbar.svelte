<script lang="ts">
	import SearchBar from "./SearchBar.svelte";
	import {auth} from "../../store/auth.store";
	import LoginButton from "./LoginButton.svelte";
    import ProfileNav from "./ProfileNav.svelte"
	const isLogged = $auth.isAuthenticated;

</script>

<nav class="bg-white border-gray-900 dark:bg-[#121C24] dark:border-[#E5E8EB] dark:border-b-2 stroke-1 sticky top-0" style="z-index: 9999">
	<div class="max-w-screen-xl flex flex-wrap items-center justify-between mx-auto p-4 w-full md:grid-cols-2">
		<div class="flex md:flex md:w-auto md:order-1">
			<a href="../" class="flex items-center space-x-3 rtl:space-x-reverse">
				<img src="/logo.svg" class="h-8 w-8" alt="AvatarUI" />
				<span class="self-center text-2xl font-bold whitespace-nowrap dark:text-white">AvatarUI</span>
			</a>
		</div>
		<div class="flex md:order-2 min-w-96">
			<SearchBar/>
			{#if isLogged}
				<ProfileNav/>
			{:else}
				<LoginButton/>
			{/if}
		</div>
	</div>
</nav>
