<script>
  import { onMount } from "svelte";
  import QRCode from "qrcode";

  export let value = "";
  export let size = 200;

  let canvas;

  onMount(() => {
    generateQR();
  });

  $: if (value) {
    generateQR();
  }

  async function generateQR() {
    if (canvas && value) {
      try {
        await QRCode.toCanvas(canvas, value, { width: size, height: size });
      } catch (err) {
        console.error(err);
      }
    }
  }
</script>

<canvas bind:this={canvas}></canvas>
