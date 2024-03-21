<script lang="ts">
    import Section from "./Section.svelte";
    import definitions from "./definitions.json";

    let {title, features} = definitions;
</script>

<Section {title} {features}/>