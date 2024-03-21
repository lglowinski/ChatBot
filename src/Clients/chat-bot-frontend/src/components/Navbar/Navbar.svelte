<script lang="ts">

	let searchTerm = '';

	const search = async () : Promise<void> => {
		console.log(`Searching for: ${searchTerm}`);
		searchTerm = "";
	}
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
			<div class="relative md:block min-w-40 w-full">
				<div class="absolute inset-y-0 start-0 flex items-center ps-3 pointer-events-none">
					<svg class="w-4 h-4 text-gray-500 dark:text-gray-400"
						 aria-hidden="true"
						 xmlns="http://www.w3.org/2000/svg" fill="none"
						 viewBox="0 0 20 20">
						<path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
							  d="m19 19-4-4m0-7A7 7 0 1 1 1 8a7 7 0 0 1 14 0Z"/>
					</svg>
					<span class="sr-only">Search icon</span>
				</div>
				<input type="text" id="search-navbar"
					   class="block w-full p-2 ps-10 text-sm text-gray-900 border border-gray-300 rounded-lg bg-gray-50
					    focus:ring-blue-500 focus:border-blue-500 dark:bg-gray-700 dark:border-gray-600
					    dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500 md:inline-block md:w-full"
					   placeholder="Search"
					bind:value={searchTerm} on:keyup={event => {if (event.key === 'Enter') search()}}
				>
			</div>

		</div>
	</div>
</nav>
