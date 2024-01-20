using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MMN_HomeWork
{
	public partial class Form1 : Form
	{
		private Bitmap image1, image2, resultImage;
		public Form1()
		{
			InitializeComponent();
			// Set PictureBox SizeMode to Zoom
			pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
			pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
			pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
		}

						private void openToolStripMenuItem_Click(object sender, EventArgs e)
						{
							// Open the first image
							OpenFileDialog openFileDialog1 = new OpenFileDialog();
							openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
							if (openFileDialog1.ShowDialog() == DialogResult.OK)
							{
								image1 = new Bitmap(openFileDialog1.FileName);
								pictureBox1.Image = image1;
							}

							// Open the second image
							OpenFileDialog openFileDialog2 = new OpenFileDialog();
							openFileDialog2.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
							if (openFileDialog2.ShowDialog() == DialogResult.OK)
							{
								image2 = new Bitmap(openFileDialog2.FileName);
								pictureBox2.Image = image2;
							}
						}

							private void readToolStripMenuItem_Click(object sender, EventArgs e)
							{
								// Check if pictureBox3 already has an image
								if (pictureBox3.Image != null)
								{
									// Ask the user for confirmation
									DialogResult result = MessageBox.Show("Are you sure you want to read a Previously written concatenated file? The current image will be replaced.",
																		  "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

									if (result == DialogResult.No)
									{
										return; // Do nothing if the user chooses not to read a new file
									}
								}

								// Read the saved image
								OpenFileDialog openFileDialog = new OpenFileDialog();
								// I commented the line down below to make the read button necessary to be used in the program
								// openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
								openFileDialog.Filter = "Image Files|*.bmp";
								if (openFileDialog.ShowDialog() == DialogResult.OK)
								{
									resultImage = new Bitmap(openFileDialog.FileName);
									pictureBox3.Image = resultImage; // Display on pictureBox3
								}
							}
								private void flipToolStripMenuItem_Click(object sender, EventArgs e)
								{
									// Flip the result image
									if (resultImage != null)
									{
										resultImage = FlipImage(resultImage);
										pictureBox3.Image = resultImage; // Update pictureBox3 as well
									}
								}

								private Bitmap FlipImage(Bitmap image)
								{
									Bitmap flippedImage = new Bitmap(image.Width, image.Height);
									for (int i = 0; i < image.Width; i++)
									{
										for (int j = 0; j < image.Height; j++)
										{
											Color p = image.GetPixel(i, j);
											flippedImage.SetPixel(i, image.Height - 1 - j, p);
										}
									}
									return flippedImage;
								}

									private void mirrorToolStripMenuItem_Click(object sender, EventArgs e)
									{
										// Mirror the result image
										if (resultImage != null)
										{
											resultImage = MirrorImage(resultImage);
											pictureBox3.Image = resultImage; // Update pictureBox3 as well
										}
									}
									private Bitmap MirrorImage(Bitmap image)
									{
										Bitmap mirroredImage = new Bitmap(image.Width, image.Height);
										for (int i = 0; i < image.Width; i++)
										{
											for (int j = 0; j < image.Height; j++)
											{
												Color p = image.GetPixel(i, j);
												mirroredImage.SetPixel(image.Width - 1 - i, j, p);
											}
										}
										return mirroredImage;
									}

										private void concatenateToolStripMenuItem_Click(object sender, EventArgs e)
										{
											if (image1 == null || image2 == null)
											{
												MessageBox.Show("Please open both images before concatenating.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
												return;
											}

											resultImage = ConcatenateImages(image1, image2);

											pictureBox3.Image = resultImage;
										}

											private Bitmap ConcatenateImages(Bitmap image1, Bitmap image2)
											{
												int width = image1.Width + image2.Width;
												int height = Math.Max(image1.Height, image2.Height);

												Bitmap concatenatedImage = new Bitmap(width, height);

												using (Graphics g = Graphics.FromImage(concatenatedImage))
												{
													g.DrawImage(image1, 0, 0);
													g.DrawImage(image2, image1.Width, 0);
												}

												return concatenatedImage;
											}
												private void saveToolStripMenuItem_Click(object sender, EventArgs e)
												{
													// Check if there is a result image to save
													if (resultImage == null)
													{
														MessageBox.Show("Please concatenate images before saving.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
														return;
													}

													// Save the result image
													SaveFileDialog saveFileDialog = new SaveFileDialog();
													//i commented the line down below to make the read button necessary to be used in the programe
													//saveFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
													saveFileDialog.Filter = "Image Files|*.bmp";

													if (saveFileDialog.ShowDialog() == DialogResult.OK)
													{
														resultImage.Save(saveFileDialog.FileName);
													}
												}
	}
}
