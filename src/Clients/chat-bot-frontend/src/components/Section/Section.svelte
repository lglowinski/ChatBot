<script lang="ts">
    export let title: string;
    export let features: { id: number; title: string; description: string; icon: string }[];
</script>

<div class="w-full flex justify-center items-center">
    <section class="py-16 dark:bg-[#121C24] text-[#F7FAFA] max-w-5xl">
        <div class="container mx-auto px-2">
            <h2 class="text-3xl font-semibold mb-6 drop-shadow">{title}</h2>
            <div class="grid grid-cols-1 md:grid-cols-4 gap-4">
                {#each features as feature (feature.id)}
                    <div class="feature bg-[#1C2936] border-1 border-2 border-[#33405C] rounded-md min-h-40 shadow">
                        <div class="icon mb-3 px-3 pt-4">
                            <img src={feature.icon} alt={feature.title} class="w-6 h-6"/>
                        </div>
                        <h3 class="text-xl text-[#F7FAFA] font-semibold mb-2 px-2">{feature.title}</h3>
                        <p class="text-[#8A9EBF] px-2">{feature.description}</p>
                    </div>
                {/each}
            </div>
        </div>
    </section>
</div>